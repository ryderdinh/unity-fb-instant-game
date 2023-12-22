1. Edit Build file

- Open file <name>.framework.js
- Find
- Find

2. Open game

> $ npm install -g http-server
> $ openssl genrsa 2048 > key.pem
> $ openssl req -x509 -days 1000 -new -key key.pem -out cert.pem
> $ http-server --ssl -c-1 -p 8080 -a 127.0.0.1

https://www.facebook.com/embed/instantgames/684934280376663/player?game_url=https://localhost:8080
