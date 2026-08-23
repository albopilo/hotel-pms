using System.Drawing;
using System.Drawing.Imaging;

namespace ComponentDll;

internal static class ImageProcessHelper
{
	private static ColorMatrix DisabledImageColorMatrix => MultiplyColorMatrix(matrix2: new float[5][]
	{
		new float[5] { 0.2125f, 0.2125f, 0.2125f, 0f, 0f },
		new float[5] { 0.2577f, 0.2577f, 0.2577f, 0f, 0f },
		new float[5] { 0.0361f, 0.0361f, 0.0361f, 0f, 0f },
		new float[5] { 0f, 0f, 0f, 1f, 0f },
		new float[5] { 0.38f, 0.38f, 0.38f, 0f, 1f }
	}, matrix1: new float[5][]
	{
		new float[5] { 1f, 0f, 0f, 0f, 0f },
		new float[5] { 0f, 1f, 0f, 0f, 0f },
		new float[5] { 0f, 0f, 1f, 0f, 0f },
		new float[5] { 0f, 0f, 0f, 0.7f, 0f },
		new float[5]
	});

	public static Image CreateDisabledImage(Image normalImage)
	{
		ImageAttributes imageAttributes = new ImageAttributes();
		imageAttributes.ClearColorKey();
		imageAttributes.SetColorMatrix(DisabledImageColorMatrix);
		Size size = normalImage.Size;
		Bitmap bitmap = new Bitmap(size.Width, size.Height);
		Graphics graphics = Graphics.FromImage(bitmap);
		graphics.DrawImage(normalImage, new Rectangle(0, 0, size.Width, size.Height), 0, 0, size.Width, size.Height, GraphicsUnit.Pixel, imageAttributes);
		graphics.Dispose();
		return bitmap;
	}

	private static ColorMatrix MultiplyColorMatrix(float[][] matrix1, float[][] matrix2)
	{
		int num = 5;
		float[][] array = new float[num][];
		for (int i = 0; i < num; i++)
		{
			array[i] = new float[num];
		}
		float[] array2 = new float[num];
		for (int j = 0; j < num; j++)
		{
			for (int k = 0; k < num; k++)
			{
				array2[k] = matrix1[k][j];
			}
			for (int l = 0; l < num; l++)
			{
				float[] array3 = matrix2[l];
				float num2 = 0f;
				for (int m = 0; m < num; m++)
				{
					num2 += array3[m] * array2[m];
				}
				array[l][j] = num2;
			}
		}
		return new ColorMatrix(array);
	}
}
