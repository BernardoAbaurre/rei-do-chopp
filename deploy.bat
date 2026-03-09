@echo off
ssh -t root@64.23.161.175 "cd rei-do-chopp && docker-compose down && docker-compose pull && docker-compose up -d --build"
pause