using System;
using System.Drawing;

namespace StegApp
{
    class StegDecode
    {
        private static int Reverse(int pix) //takes pixel value and reverses it 
        {
            int a = 0;
            for (int height = 0; height < 8; height++) //reverse value
            {
                if ((pix & (1 << height)) != 0)
                    a |= 1 << (7 - height);
            }
            return a;
        }
        public static string StegDecoding(Bitmap bitmp)
        {
            string decodedText = ""; //stores the decoded teg that will be returned
            int colour = 0; //index of colour unit from image
            int value = 0; //store the intger, converted from character to hide
            int height; //height of the image
            int width;  //width
            Color pixel; //stores the current pixel in the processing

            for (height = 0; height < bitmp.Height; height++) //go through all the height pixels of the image
            {
                for (width = 0; width < bitmp.Width; width++) //go through the width pixels
                {
                    int pix;
                    pixel = bitmp.GetPixel(width, height);  //get the pixels from the decoded image
                    for (pix = 0; pix < 3; pix++) //for every pixel, go through the RGB elements
                    {
                        try
                        {
                            if (colour % 3 == 0) //divide by 3 and use the remainder, can be either 0, 1 or 2
                            {
                                Console.WriteLine("decode value " + value);
                                value = value * 2 + pixel.R % 2; //read from the pixel element then add a bit to right of current character value
                            }
                            else if (colour % 3 == 1) //remainder 1
                            {
                                value = value * 2 + pixel.G % 2; //read from the pixel element and add to value 
                            }
                            else if (colour % 3 == 2) //remainder 2
                            {
                                value = value * 2 + pixel.B % 2; //whilst decoding we add and multiply, whilst encoding we divided and decode; same process reversed. 
                                Console.WriteLine("decode value " + value);
                            }

                            colour++; //keep going until colour unit gets to 8.
                            
                            if (colour % 8 == 0) // check for consecutive 8 bits, then we know the encoded data is all read.
                            {
                                value = Reverse(value); //need to reverse the value because the process takes place on the right
                                if (value == 0) //if the value is 0 then it indicates it is the end because we added zeroes
                                    return decodedText; //return decoded text
                                char character = (char)value; //convert character value to character
                                decodedText += character.ToString(); //coventry characters back to string
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                }
            }
            return decodedText; //returnt the plain text
        }
    }
}
