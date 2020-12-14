FROM node:13 as build

WORKDIR /opt/ng
COPY package.json package-lock.json ./

RUN npm install

COPY . ./

RUN npm run build:prod

FROM nginx:1.19-alpine as runtime

RUN rm -rf /usr/share/nginx/html/*

COPY ./docker/nginx.conf /etc/nginx/nginx.conf

COPY --from=build  /opt/ng/dist/effectory /usr/share/nginx/html

RUN echo "mainFileName=\"\$(ls /usr/share/nginx/html/main*.js)\" && \
          envsubst '\$BACKEND_API_URL' < \${mainFileName} > main.tmp && \
          mv main.tmp  \${mainFileName} && nginx -g 'daemon off;'" > run.sh

ENTRYPOINT ["sh", "run.sh"]
