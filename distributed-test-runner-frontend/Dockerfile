FROM node:18-alpine
EXPOSE 3000
WORKDIR /dtr-frontend/
COPY public/ /dtr-frontend/public
COPY src/ /dtr-frontend/src
COPY package.json /dtr-frontend/
RUN npm install
CMD ["npm", "start"]