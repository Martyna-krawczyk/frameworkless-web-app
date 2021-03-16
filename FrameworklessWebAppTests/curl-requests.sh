#!/usr/bin/env bash
set -e

URL=${1:-"https://martyna-web-app.svc.platform.myobdev.com"}
CONTENT_TYPE="Content-Type:application/x-www-form-urlencoded"

echo -e "\nGet Greeting-"
curl -X GET "$URL" 

echo -e "\nAdd User-"
curl --request POST "$URL/users" \
--header $CONTENT_TYPE \
--data-urlencode 'Name=Marcelo'

echo -e "\nGet List Of Names-"
curl -X GET "$URL/users" 

echo -e "\nGet Single User-"
curl -X GET "$URL/users/marcelo" 

echo -e "\nUpdate User-"
curl --request PUT "$URL/users/marcelo" \
--header $CONTENT_TYPE \
--data-urlencode 'Name=Emile'

echo -e "\nGet List Of Names-"
curl -X GET "$URL/users" 

echo -e "\nGet Greeting-"
curl -X GET "$URL" 

echo -e "\nDelete User-"
curl --request DELETE "$URL/users/emile" 
echo -e "\n"