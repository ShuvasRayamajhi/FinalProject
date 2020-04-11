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
            bool add = false; //boolean variable to decide whether to hide or add, false = hide, true = fill with zeroes 
            int height, width; //store height width of image
            getImageData(out long pixel, out int zero, out _, out _, out _, out int character, out int value, out _); //call method

            for (height = 0; height < image.Height; height++) //go through the height of the bmp.
            {
                for (width = 0; width < image.Width; width++) //go through the width of the bmp.
                {
                    removeLSB(image, out int Blue, out int Green, out int Red, height, width); //remove the least significant bit from each pixel
                    for (int pix = 0; pix < 3; pix++) //go through each pixel for RGB and turn their value to 0
                    {
                        if (pixel % 8 == 0) //each pixel has three values; red, green, and blue. Each RGB is 8bit so we can add 8 zeroes in each RGB value
                        {
                            if (add == true && zero == 8)  // process finished when 8 zeroes have been added. Using 8 zeroes to indicate that end of the text is reached so we can decode. 
                            {
                                pixel = (pixel - 1) % 3;
                                if (pixel < 2) // RGB values changed to 0 are not applied to the bmp object, and when finished, the changed pixel values are applied
                                    image.SetPixel(width, height, Color.FromArgb(Blue, Green, Red)); //apply adding zero to the image
                                return image; //return the encoded image
                            }

                            if (character >= text.Length) //ensures that the every character from the text is hidden
                                add = true; //keep adding zeroes until it reaches the text length

                            else //if there is still text to hide, bring one more character to hide.
                                value = text[character++]; //convert the current character to an integer, iterate to the next character in the text and repeat adding process
                        }

                        //hide 8 bits of each character consecutively into the RGB (eg. R1 to R3) the number is the pixels. Do this for all bits of characters until text is finished. 
                        switch (pixel % 3) //each pixel contains three values (red, green, blue), these are 8bit values. 
                        {
                            case 0:
                                {
                                    if (add == false) //when zeroes have stopped adding, we start the hiding of the text 
                                        Blue = Blue + value % 2; //  add 2 percent of character to Blue bits
                                    value = value / 2; //divided by value by 2
                                }
                                break;
                            case 1:
                                {
                                    if (add == false)
                                        Green += value % 2; // add 2 percent of characterto Green pixels
                                    value /= 2; //divide character value by 2, 
                                }
                                break;
                            case 2:
                                {
                                    if (add == false)
                                        Red += value % 2;  // add 2 percent of character to Green pixels
                                    value /= 2; //divide character value by 2, 

                                    image.SetPixel(width, height, Color.FromArgb(Blue, Green, Red)); // Save R G B values in pixels at height and width position.
                                }
                                break;
                        }

                        pixel++;
                        if (add == true)
                            zero++; //loop until amount of zeroes added is 8
                    }
                }
            }
            return image;
        }

        private static void removeLSB(Bitmap image, out int Blue, out int Green, out int Red, int height, int width)
        {
            Color pixel = image.GetPixel(width, height); //stores the current pixel in the processing
            Red = pixel.R - pixel.R % 2; //remove the red LSB; 2% of red pixels
            Green = pixel.G - pixel.G % 2; //remove the green LSB
            Blue = pixel.B - pixel.B % 2; // remove blue LSB
        }

        private static void getImageData(out long pixel, out int zero, out int Red, out int Green, out int Blue, out int character, out int value, out int pix)
        {
            pixel = 0; //store the integer index of the pixel 
            zero = 0; //store amount of  zeroes that have been added
            character = 0; // store interger index of the particular character to hide
            value = 0; //store the intger, converted from character to hide
            Blue = 0; // store blue pixels
            Red = 0; //store red pixels
            Green = 0; //stor green pixels
            pix = 0; //used to check through pixels 
        }
    }
}
