FROM nvidia/cuda:10.0-cudnn7-runtime
LABEL maintainer="Denis Pettens <denis.pettens@gmail.com>"

# Env variables for build
ARG OPENCV_VERSION=3.3.1
ARG DOTNET_SDK_VERSION=2.1.500
ARG DEBIAN_FRONTEND=noninteractive

# OPENCV
WORKDIR /

RUN apt-get update \
    && apt-get install -y g++ wget unzip cmake zlib1g-dev libjpeg-dev \
    libpng-dev libavcodec-dev libavformat-dev libswscale-dev libavresample-dev \
    libx264-dev libv4l-dev libxine2-dev \
    && wget https://github.com/opencv/opencv/archive/${OPENCV_VERSION}.zip \
    && unzip ${OPENCV_VERSION}.zip && rm ${OPENCV_VERSION}.zip \
    && mv opencv-${OPENCV_VERSION} OpenCV \
    && wget https://github.com/opencv/opencv_contrib/archive/${OPENCV_VERSION}.zip \
    && unzip ${OPENCV_VERSION}.zip && rm ${OPENCV_VERSION}.zip \
    && mv opencv_contrib-${OPENCV_VERSION} OpenCV_contrib \
    && mkdir OpenCV/build && cd OpenCV/build \
    && cmake \
        -D CMAKE_BUILD_TYPE=RELEASE \
        -D OPENCV_EXTRA_MODULES_PATH=../../OpenCV_contrib/modules \
        -D WITH_WEBP=OFF\
        -D WITH_JASPER=OFF \
        -D WITH_TIFF=OFF \
        -D WITH_OPENEXR=OFF \
        -D WITH_CUDA=OFF \
        -D WITH_VTK=OFF \
        -D WITH_GTK=OFF \
        -D WITH_MATLAB=OFF \
        -D BUILD_EXAMPLES=OFF \
        -D BUILD_DOCS=OFF \
        -D BUILD_TESTS=OFF \
        -D BUILD_PERF_TESTS=OFF \
        -D BUILD_opencv_java=OFF \
        -D BUILD_opencv_python=OFF \
        .. \
    && make -j $(nproc) \
    && make install \
    && ldconfig \
    && cd ../../ && rm -rf OpenCV OpenCV_contrib \
    && apt-get remove -y --purge g++ wget unzip cmake \
    && apt-get autoremove -y \
    && apt-get clean autoclean \
    && rm -rf /var/lib/{apt,dpkg,cache,log}/

# DOTNET CORE (From https://github.com/dotnet/dotnet-docker)
WORKDIR /

RUN apt-get update \
    && apt-get install -y --no-install-recommends wget libc6 libgcc1 libicu60 \
    libgssapi-krb5-2 liblttng-ust0 libssl1.0.0 libstdc++6 zlib1g \
    && wget -O dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/$DOTNET_SDK_VERSION/dotnet-sdk-$DOTNET_SDK_VERSION-linux-x64.tar.gz \
    && mkdir -p /usr/share/dotnet \
    && tar -zxf dotnet.tar.gz -C /usr/share/dotnet \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    && apt-get remove -y --purge wget \
    && apt-get autoremove -y \
    && apt-get clean autoclean \
    && rm -rf /var/lib/{apt,dpkg,cache,log}/

ENV ASPNETCORE_URLS=http://+:80 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_USE_POLLING_FILE_WATCHER=true \
    NUGET_XMLDOC_MODE=skip

# Fix problem when a lib can't find another in the same directory
ENV LD_LIBRARY_PATH=$LD_LIBRARY_PATH:./