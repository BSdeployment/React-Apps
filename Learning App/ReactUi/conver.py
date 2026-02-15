from PIL import Image

img = Image.open(r"C:\Users\USER001\Downloads\Gemini_Generated_Image_yfouyfyfouyfyfou.png")

img = img.resize((256,256))

img.save(r"iconapp.ico")