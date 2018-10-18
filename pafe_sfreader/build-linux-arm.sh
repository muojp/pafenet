#!/bin/bash

mkdir -p build-linux-arm
cd build-linux-arm && \
  cmake -D CMAKE_INSTALL_PREFIX=../pafe_sfreader_bin .. && \
  make
