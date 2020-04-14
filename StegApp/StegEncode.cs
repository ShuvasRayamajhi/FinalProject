using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegApp
{
    class StegEncode
    {
        public static Bitmap Encoding(string text, Bitmap image)
        {
            bool addZero = false; //boolean variable to decide whether to hide or add, false = hide, true = fill with zeroes 
            int height, width; //store height width of image
            GetImageData(out long pixel, out int zero, out _, out _, out _, out int character, out int value, out _); //call method

            for (height = 0; height < image.Height; height++) //go through the height of the bmp.
            {
                for (width = 0; width < image.Width; width++) //go through the width of the bmp.
                {
                    GetLSB(image, out int blueLSB, out int greenLSB, out int redLSB, height, width); //remove the least significant bit from each pixel
                    for (int pix = 0; pix < 3; pix++) //go through each pixel for RGB and turn their value to 0
                    {
                        try
                        {
                            if (pixel % 8 == 0) //each pixel has three values; red, green, and blue. Each RGB is 8bit so we can add 8 zeroes in each RGB value
                            {
                                if (addZero == true && zero == 8)  // process finished when 8 zeroes have been added. Using 8 zeroes to indicate that end of the text is reached so we can decode. 
                                {
                                    Console.WriteLine(blueLSB + redLSB + greenLSB + " 1. lsb before");
                                    pixel = (pixel - 1) % 3;
                                    if (pixel < 2) //this is only activated for the last pixel at the end
                                        image.SetPixel(width, height, Color.FromArgb(blueLSB, greenLSB, redLSB)); //apply to the last pixel if required in the image  
                                    
                                    return image; //return the encoded image
                                }

                                if (text.Length <= character ) //ensures that the every character from the text is hidden
                                    addZero = true; //keep adding zeroes until it reaches the text length

                                else //if there is still text to hide, bring one more character to hide.
                                    value = text[character++]; //convert the current character to an integer, iterate to the next character in the text and repeat adding process
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                        try
                        {
                            //hide 8 bits of each character consecutively into the RGB (eg. R1 to R3) the number is the pixels. Do this for all bits of characters until text is finished. 
                            Console.WriteLine(blueLSB + redLSB + greenLSB + " 2. lsb before");
                        
                            if (pixel % 3 == 0)
                            {
                                if (addZero == false) //when zeroes have stopped adding, we start the hiding of the text
                                    blueLSB += value % 2; //  add  remainder of character value to blue LSB bits; if the value is odd it will 1, if the value even it will be 0
                                    value /= 2; //divide value by 2 and set that to the new value
                            }
                            else if (pixel % 3 == 1)
                            {
                                if (addZero == false)
                                    greenLSB += value % 2; //add  remainder of character value to greenLSB pixels
                                    value /= 2; //divide character value by 2, 
                            }
                            else if (pixel % 3 == 2)
                            {
                                if (addZero == false)
                                    redLSB += value % 2;  // add  remainder of character value to red pixels
                                    value /= 2; //divide character value by 2, 
                                    image.SetPixel(width, height, Color.FromArgb(blueLSB, greenLSB, redLSB)); // apply to image the changes
                            }

                            Console.WriteLine(value + " value after");
                            pixel++;
                            if (addZero == true)
                                zero++; //loop until amount of zeroes added is 8
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                }
            }
            return image; //return the encoded image
        }

        private static void GetLSB(Bitmap image, out int blueLSB, out int greenLSB, out int redLSB, int height, int width)
        {
            Color pixel = image.GetPixel(width, height); //stores the current pixel in the processing
            
            redLSB = pixel.R & ~1; //get the red LSB and force it 0 
            greenLSB = pixel.G & ~1; //get the green LSB and force it 0
            blueLSB = pixel.B & ~1; // get blue LSB and force it 0
        }

        private static void GetImageData(out long pixel, out int zero, out int redLSB, out int greenLSB, out int blueLSB, out int character, out int value, out int pix)
        {
            pixel = 0; //store the integer index of the pixel 
            zero = 0; //store amount of  zeroes that have been added
            character = 0; // store interger index of the particular character to hide
            value = 0; //store the intger, converted from character to hide
            blueLSB = 0; // store blue pixels
            redLSB = 0; //store red pixels
            greenLSB = 0; //stor green pixels
            pix = 0; //used to check through pixels 
        }
    }
}
