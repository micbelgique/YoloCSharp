#include "yolo_v2_class.hpp"
#include <iostream>
#include <opencv2/opencv.hpp>

#define YOLOAPI_EXPORT __attribute__((visibility("default")))

extern "C" {
	/**
	* Purpose: Initializes a new instance of the Darknet detector
	*
	* @param cfg_filename Relative path to the config file
	* @param weight_filename Relative path to the weights file
	* @param gpu_id The CUDA compatible GPU index
	*/
	YOLOAPI_EXPORT void initDetector(char* cfg_filename, char* weight_filename, int gpu_id);

	/**
	* Purpose: Destroys the Darknet detector instance, ready to be re-initialized
	*/
	YOLOAPI_EXPORT void resetDetector();

	/**
	* Purpose: Launches the detection process on a give image and returns its bounding boxes
	*
	* @param image_filename Relative path to the image to process
	* @param array_size Amount of objects detected on the image
	* @param thresh Threshold at which to detect objects
	* @param use_mean
	* @return Array of all the bounding boxes for each detected object
	*/
	YOLOAPI_EXPORT bbox_t* detect(char* image_filename, int* array_size, float thresh = 0.2, bool use_mean = false);

#ifdef OPENCV
	/**
	* Purpose: Launches the detection process on a give image and returns its bounding boxes
	*
	* @param mat_data Pointer to a cv::Mat object containing the frame to be processed
	* @param elems Returned bounding boxes of each detected object on an image
	* @param array_size Amount of objects detected on the image
	* @param mat_rows Rows of the cv::Mat object
	* @param mat_cols Columns of the cv::Mat object
	* @param thresh Threshold at which to detect objects
	* @param use_mean
	*/
	YOLOAPI_EXPORT void detectOpenCV(unsigned char* mat_data, bbox_t** elems, int* array_size, int mat_rows, int mat_cols, float thresh = 0.2, bool use_mean = false);
#endif
}

static Detector* instance = NULL;

YOLOAPI_EXPORT void initDetector(char* cfg_filename, char* weight_filename, int gpu_id) {
	instance = new Detector(std::string(cfg_filename), std::string(weight_filename), gpu_id);
}

YOLOAPI_EXPORT void resetDetector() {
	if (instance != NULL) {
		delete instance;
		instance = 0;
	}
}

YOLOAPI_EXPORT bbox_t* detect(char* image_filename, int* array_size, float thresh, bool use_mean) {
	std::vector<bbox_t> resultV = instance->detect(image_filename, thresh, use_mean);
	bbox_t* result = &(resultV)[0];
	*array_size = ((int)resultV.size());
	return result;
}

#ifdef OPENCV

YOLOAPI_EXPORT void detectOpenCV(unsigned char* matData, bbox_t** elems, int* arraySize, int matRows, int matCols, float thresh, bool use_mean) {
	cv::Mat imgMat = cv::Mat(matRows, matCols, CV_8UC3, matData);
	std::vector<bbox_t> resultV = instance->detect(imgMat, thresh, use_mean);
	*elems = resultV.data();
	*arraySize = resultV.size();
}
#endif
