#!/bin/bash

# Apply first config file
kubectl apply -f ./connection-string-config.yaml

# Apply second config file
kubectl apply -f ./config.yaml

# Apply readines probe yaml file
kubectl apply -f ./deployment-readiness.yaml