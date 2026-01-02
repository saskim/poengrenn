FROM mcr.microsoft.com/devcontainers/base:ubuntu-22.04

ARG DOTNET_VERSION=10.0.101
ARG NODE_VERSION=8.17.0
ARG ANGULAR_CLI_VERSION=1.0.2

SHELL ["/bin/bash", "-c"]

RUN apt-get update && apt-get install -y --no-install-recommends \
    ca-certificates \
    curl \
    git \
  && rm -rf /var/lib/apt/lists/*

RUN curl -fsSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh \
  && bash /tmp/dotnet-install.sh --version ${DOTNET_VERSION} --install-dir /usr/share/dotnet \
  && ln -s /usr/share/dotnet/dotnet /usr/local/bin/dotnet \
  && rm /tmp/dotnet-install.sh

ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH="/usr/share/dotnet:${PATH}"

ENV NVM_DIR=/usr/local/share/nvm
RUN mkdir -p ${NVM_DIR} \
  && curl -fsSL https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.7/install.sh -o /tmp/nvm-install.sh \
  && bash /tmp/nvm-install.sh \
  && source ${NVM_DIR}/nvm.sh \
  && nvm install ${NODE_VERSION} \
  && nvm alias default ${NODE_VERSION} \
  && nvm use default \
  && npm install -g @angular/cli@${ANGULAR_CLI_VERSION} \
  && chmod -R a+rX ${NVM_DIR} \
  && rm /tmp/nvm-install.sh

ENV PATH="${NVM_DIR}/versions/node/v${NODE_VERSION}/bin:${PATH}"
