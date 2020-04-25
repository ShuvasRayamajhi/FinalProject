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
            GetImageData(out long pixelIndex, out int zero, out _, out _, out _, out int character, out int characterValue, out _); //call method

            for (height = 0; height < image.Height; height++) //go through the height of the bmp.
            {
                for (width = 0; width < image.Width; width++) //go through the width of the bmp.
                {
                    GetLSB(image, out int blueLSB, out int greenLSB, out int redLSB, height, width); //remove the least significant bit from each image pixel                    
                    for (int pix = 0; pix < 3; pix++) //go through each pixelIndex for RGB and turn their characterValue to 0
                    {
                        try
                        {
                            if (addZero == true && pixelIndex % 8 == 0)  // process finished when 8 zeroes have been added. Using 8 zeroes to indicate that end of the text is reached so we can decode. 
                            {
                                Console.WriteLine(blueLSB + redLSB + greenLSB + " 1. lsb before");
                                Console.WriteLine("pixelIndex" + pixelIndex);
                                pixelIndex = (pixelIndex - 1) % 3; //remainder of pixelIndex - 1 divided by 3
                                if (pixelIndex <= 1) //this is only activated for the last pixelIndex at the end
                                    image.SetPixel(width, height, Color.FromArgb(blueLSB, greenLSB, redLSB)); //apply to the last pixel Index if required in the image  
                                    
                                return image; //return the encoded image
                            }

                            if (text.Length <= character && pixelIndex % 8 == 0) //ensures that the every character from the text is hidden
                                addZero = true; //keep adding zeroes until it reaches the text length

                            else if (pixelIndex % 8 == 0) //if there is still text to hide, bring one more character to hide.
                                characterValue = text[character++]; //convert the current character to an integer, iterate to the next character in the text and repeat adding process
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        try
                        {
                            if (pixelIndex % 3 == 0) //pixel divided by 3 leaves 0 remainder
                            {
                                if (addZero == false) //when zeroes have stopped adding, we start the hiding of the text
                                    blueLSB += characterValue % 2; //find the rightmost bit in the character value; this will replace the LSB of blue pixel element, 
                                    characterValue /= 2; //half the character value
                            }
                            else if (pixelIndex % 3 == 1) //pixel divided by 3 leaves 1 remainder
                            {
                                if (addZero == false)
                                    greenLSB += characterValue % 2; ////find the rightmost bit in the character value; this will replace the LSB of green pixel element
                                    characterValue /= 2; //half the characterValue
                            }
                            else if (pixelIndex % 3 == 2) // leaves 2 remainder
                            {
                                if (addZero == false)
                                    redLSB += characterValue % 2;  // //find the rightmost bit in the character value; this will replace the LSB
                                    characterValue /= 2; //half the characterValue 
                                    image.SetPixel(width, height, Color.FromArgb(blueLSB, greenLSB, redLSB)); // apply to image the changes
                            }
                            pixelIndex++;
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
            Color pixelIndex = image.GetPixel(width, height); //stores the current pixelIndex in the processing
            redLSB = pixelIndex.R & ~1; //get the red LSB and force it 0 
            greenLSB = pixelIndex.G & ~1; //get the green LSB and force it 0
            blueLSB = pixelIndex.B & ~1; // get blue LSB and force it 0
        }

        private static void GetImageData(out long pixelIndex, out int zero, out int redLSB, out int greenLSB, out int blueLSB, out int character, out int characterValue, out int pix)
        {
            pixelIndex = 0; //store the integer index of the pixelIndex 
            zero = 0; //store amount of  zeroes that have been added
            character = 0; // store interger index of the particular character to hide
            characterValue = 0; //store the intger, converted from character to hide
            blueLSB = 0; // store blue pixels
            redLSB = 0; //store red pixels
            greenLSB = 0; //stor green pixels
            pix = 0; //used to check through pixels 
        }
    }
}
