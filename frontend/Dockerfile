FROM node:24.14.0-alpine3.23 AS builder
WORKDIR /source
COPY . .
RUN npm ci
RUN npm run build

FROM nginx:1.29.6-alpine3.23 AS runner
WORKDIR /app
COPY --from=builder /source/dist /app
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

HEALTHCHECK --interval=30s --timeout=5s --start-period=5s --retries=3 \
  CMD wget --no-verbose --tries=1 --spider http://localhost/healthz || exit 1

ENTRYPOINT ["/docker-entrypoint.sh"]
CMD ["nginx", "-g", "daemon off;"]
