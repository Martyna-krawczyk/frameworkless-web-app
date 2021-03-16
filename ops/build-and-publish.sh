#! /usr/bin/env bash
set -eu

appImage="741922737521.dkr.ecr.ap-southeast-2.amazonaws.com/martyna-web-app:${IMAGE_TAG}"
localRef="martyna-web-app:latest"

echo "--- Login to ECR" 
$(aws ecr get-login --no-include-email --region ap-southeast-2)

echo "--- Build app image"
docker-compose build app

echo "--- Test app image"
./FrameworklessWebAppTests/curl-requests-container-level.sh

echo "--- Publish app image"
docker tag $localRef $appImage 
docker push $appImage