cmake_minimum_required(VERSION 3.7.2)

project(pafe_sfreader LANGUAGES C)

add_library(pafe_sfreader SHARED
    pafe_sfreader.c
    )

add_subdirectory(libpafe)
target_link_libraries(pafe_sfreader PRIVATE pafe)
include_directories(libpafe/src/)
# install(
#   TARGETS pafe_sfreader
#   ARCHIVE DESTINATION bin
#   LIBRARY DESTINATION lib
# )
