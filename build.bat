@echo off
setlocal enabledelayedexpansion

:: === CONFIGURAÇÕES ===
set GHCR_USERNAME=bernardoabaurre
set GHCR_TOKEN=%GHCR_TOKEN%

:: Caminhos e nomes das imagens
set FRONT_IMAGE=ghcr.io/%GHCR_USERNAME%/rei-do-chopp-site:latest
set BACK_IMAGE=ghcr.io/%GHCR_USERNAME%/rei-do-chopp-api:latest
set FRONT_CONTEXT=rei-do-chopp-site
set BACK_CONTEXT=rei-do-chopp-api
set FRONT_DOCKERFILE=Dockerfile.angular
set BACK_DOCKERFILE=Dockerfile.api

:: === LOGIN ===
echo.
echo [LOGIN] Fazendo login no GHCR...
echo %GHCR_TOKEN% | docker login ghcr.io -u %GHCR_USERNAME% --password-stdin
if errorlevel 1 (
    echo [ERRO] Falha no login no GHCR. Verifique o token.
    goto :error
)

:: === BUILD FRONTEND ===
echo.
echo ========== BUILD FRONTEND ==========
echo [BUILD] Criando imagem do FRONTEND...
docker build -t %FRONT_IMAGE% -f %FRONT_DOCKERFILE% %FRONT_CONTEXT%
if errorlevel 1 (
    echo [ERRO] Falha no build do frontend.
    goto :error
)

:: === BUILD BACKEND ===
echo.
echo ========== BUILD BACKEND ==========
echo [BUILD] Criando imagem do BACKEND...
docker build -t %BACK_IMAGE% -f %BACK_DOCKERFILE% %BACK_CONTEXT%
if errorlevel 1 (
    echo [ERRO] Falha no build do backend.
    goto :error
)

:: === PUSH FRONTEND ===
echo.
echo ========== PUSH FRONTEND ==========
echo [UPLOAD] Enviando imagem do FRONTEND para o GHCR...
docker push %FRONT_IMAGE%
if errorlevel 1 (
    echo [ERRO] Falha ao enviar imagem do frontend.
    goto :error
)

:: === PUSH BACKEND ===
echo.
echo ========== PUSH BACKEND ==========
echo [UPLOAD] Enviando imagem do BACKEND para o GHCR...
docker push %BACK_IMAGE%
if errorlevel 1 (
    echo [ERRO] Falha ao enviar imagem do backend.
    goto :error
)

:: === FINALIZAÇÃO ===
echo.
echo ========== BUILD FINALIZADO ==========
echo [SUCESSO] Imagens do front-end e back-end atualizadas com sucesso no GHCR!
endlocal
pause
exit /b 0

:error
echo [ERRO] Ocorreu um erro. Revise as mensagens acima.
endlocal
pause
exit /b 1