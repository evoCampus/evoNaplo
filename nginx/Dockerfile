FROM nginx:1.29.6-alpine3.23

RUN apk add apache2-utils curl

ENV NGINX_ENVSUBST_TEMPLATE_DIR=/app

RUN mkdir /app

COPY nginx.conf.template /app/nginx.conf.template

COPY auth.sh /docker-entrypoint.d/auth.sh
RUN chmod +x /docker-entrypoint.d/auth.sh

EXPOSE 80

HEALTHCHECK --interval=30s --timeout=5s --start-period=5s --retries=3 CMD [ "/healthz" ]

ENTRYPOINT [ "/docker-entrypoint.sh" ]

CMD [ "nginx", "-g", "daemon off;" ]