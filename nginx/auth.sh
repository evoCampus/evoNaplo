#!/bin/sh

USER=${AUTH_USER}
PASS=${AUTH_PASSWORD}

if [ -z "$USER" ] && [ -z "$PASS" ]; then
    exit 0
fi

if [ -z "$USER" ] || [ -z "$PASS" ]; then
    echo "ERROR: One of data was not provided."
    exit 1
fi

htpasswd -cb /etc/nginx/.htpasswd "$USER" "$PASS"

echo "Authentication was created!"

cat <<EOF > /etc/nginx/conf.d/auth_basic.conf
auth_basic "Restricted Access";
auth_basic_user_file /etc/nginx/.htpasswd;
EOF