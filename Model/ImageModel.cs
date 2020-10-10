﻿using System.IO;
using OpenCvSharp;

namespace OpenCVTester.Model
{
    public class ImageModel
    {
        private ImageProcessing _imageProcessing;
        private Mat _originImage;
        private Mat _currentImage;
        private Mat _histogram;
        private int _brightness;
        private double _contrast;

        private Mat MakeCurrentImage()
        {
            Mat tempImage = _originImage;
            tempImage = _imageProcessing.ControlBrightness(tempImage, _brightness);
            tempImage = _imageProcessing.ChangeContrast(tempImage, _contrast);
            _currentImage = tempImage;
            return _currentImage;
        }

        public Mat RegisterImage(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                _originImage = new Mat(imagePath, ImreadModes.Grayscale);
                _currentImage = _originImage;
                return _currentImage;
            }
            else
            {
                return null;
            }
        }
        public Mat GetImage()
        {
            return _currentImage;
        }
        public Mat GetHistogram()
        {
            return _histogram;
        }
        public int GetBrightness()
        {
            return _brightness;
        }
        public double GetContrast()
        {
            return _contrast;
        }
        public Mat CalculateHistogram()
        {
            _histogram = _imageProcessing.CalculateHistogram(_currentImage);
            return _histogram;
        }
        public Mat ControlBrightness(int value)
        {
            _brightness = value;
            return MakeCurrentImage();
        }
        public Mat ChangeContrast(double value)
        {
            _contrast = value;
            return MakeCurrentImage();
        }
        public Mat AddWeightedImages(Mat image1, Mat image2, double alpha, double beta)
        {
            Cv2.AddWeighted(image1, alpha, image2, beta, 0, _originImage);
            _currentImage = _originImage;
            return _currentImage;
        }
        public Mat SubtractImages(Mat image1, Mat image2)
        {
            Cv2.Subtract(image1, image2, _originImage);
            _currentImage = _originImage;
            return _currentImage;
        }
        public Mat AbsDiff(Mat image1, Mat image2)
        {
            Cv2.Absdiff(image1, image2, _originImage);
            _currentImage = _originImage;
            return _currentImage;
        }
        public Mat NormalizeHistogram()
        {
            _currentImage = _currentImage.Normalize(0, 255, NormTypes.MinMax);
            return _currentImage;
        }
        public Mat EqualizeHistogram()
        {
            _currentImage = _currentImage.EqualizeHist();
            return _currentImage;
        }

        public ImageModel()
        {
            _imageProcessing = new ImageProcessing();
            _originImage = new Mat();
            _currentImage = new Mat();
            _histogram = new Mat();
        }
    }
}
