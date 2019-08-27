#!/bin/bash

docker create --name test-stage $(docker images --filter "label=test=true" -q | head -1)
docker cp test-stage:/test ./test
docker rm test-stage