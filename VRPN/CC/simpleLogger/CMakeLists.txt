# --------------------------------------------------------------------------
# Logging von Server-Daten in eine Datei
# --------------------------------------------------------------------------
cmake_minimum_required(VERSION 3.13)
project(VRPNLogger)

# Allgemeine Optionen
include(CMakeOptions.txt)

# Alle Quellen in das Projekt aufnehmen
file(GLOB SOURCES "*.cpp")
file(GLOB HEADER "*.h")

# Pfade für die Verwendung von VRPN 
# Passen Sie die Pfade auf Ihre Installation an!
# Die Verwendung von C:\local\lib und C:\local\include ist ein Versuch,
# /usr/local/ auf UNIX zu simulieren.
# Diese Wahl hat insbesondere den Vorteil, dass Sie dort ein install
# machen können, ohne das Windows mit Rückfragen nervt ...
if(WIN32)
    set(VRPN_LIB_DIR "C:/local/lib" CACHE PATH "VRPN library path")
    set(VRPN_HEADER_DIR "C:/local/include" CACHE PATH "VRPN header path")
elseif(WIN64)
    set(VRPN_LIB_DIR "C:/local/lib" CACHE PATH "VRPN library path")
    set(VRPN_HEADER_DIR "C:/local/include/vrpn" CACHE PATH "VRPN header path")
elseif(APPLE)
    set(VRPN_LIB_DIR "/Developer/usr/local/lib" CACHE PATH "VRPN library path")
    set(VRPN_HEADER_DIR "/Developer/usr/local/include" CACHE PATH "vRPN header path")
elseif(UNIX)
    set(VRPN_LIB_DIR "/usr/local/lib" CACHE PATH "VRPN library path")
    set(VRPN_HEADER_DIR "/usr/local/include/vrpn" CACHE PATH "VRPN header path")
endif()

# Verzeichnisse für die Header
include_directories(${VRPN_HEADER_DIR})
# Verzeichnisse für den Linker
link_directories(${VRPN_LIB_DIR})

# Ausführbares Programm
add_executable (simpleLogger ${SOURCES} ${HEADER})

TARGET_LINK_LIBRARIES(simpleLogger vrpn)
