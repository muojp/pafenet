#!/bin/bash

mkdir -p build-linux-x64
cd build-linux-x64 && \
  cmake -D CMAKE_INSTALL_PREFIX=../pafe_sfreader_bin .. && \
  make
