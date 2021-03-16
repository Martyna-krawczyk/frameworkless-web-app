#!/usr/bin/env bash
set -e

echo "Build and run app" 
docker-compose up --detach app

echo "Run curl commands against container"
docker-compose run debug-test curl-requests.sh "http://martyna-app-run:8080"

echo "Stop and remove all containers running from docker-compose file"
docker-compose down