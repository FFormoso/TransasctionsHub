#!/bin/bash
# Usage: deploy.sh SERVICE [CMD...]

project="transactions-hub-3"

flags=()
profile=""

for arg in "$@"; do
  if [[ "$arg" == -* ]]; then
    flags+=("$arg")
  else
    profile="$arg"
  fi
done

# Run the containers with the indicated profile
docker-compose -p "$project" --profile $profile up -d "${flags[@]}"
exit_code=$?

echo "Waiting for auxiliary containers to finish..."

# Wait for all containers with "aux" in the name to be in exited state
while true; do
  aux_running=$(docker ps --filter "name=aux-*" --filter "status=running" -q)
  aux_created=$(docker ps --filter "name=aux-*" --filter "status=created" -q)

  if [ -z "$aux_running" ] && [ -z "$aux_created" ]; then
    echo "All auxiliary container finished."
    break
  fi
    sleep 1

done

# Removes stopped auxiliary containers
aux_exited=$(docker ps -a --filter "name=aux-*" --filter "status=exited" -q)

if [ -n "$aux_exited" ]; then
  echo "Removing stopped axuliary containers..."
  docker rm $aux_exited
fi

exit $exit_code