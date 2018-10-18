md build-release-win64
cd build-release-win64
cmake -G "Visual Studio 15 2017 Win64" -DCMAKE_WINDOWS_EXPORT_ALL_SYMBOLS=TRUE ..
"C:\Program Files\CMake\bin\cmake.EXE" --build . --config Release --target pafe_sfreader