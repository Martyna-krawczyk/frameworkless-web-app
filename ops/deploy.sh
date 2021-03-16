#! /usr/bin/env bash

echo "--- Deploying to Europa Preprod"

ktmpl ./ops/deployment.yml -p imageTag $IMAGE_TAG | kubectl apply -f -