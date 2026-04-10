#!/bin/sh

USER = {$AUTH_USER};
PASS = {$AUTH_PASSWORD}

if [ -z "$USER" ] && [ -z "$PASS" ]; Then
    exit 0
fi

if [ -z "$USER" ] || [ -z "$PASS" ]; Then
    echo "One of data was not provided."
    exit 0
fi

htpasswd -cb /etc/nginx/.htpasswd "$USER" "$PASS"

echo "Authentication was created!"
