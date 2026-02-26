from PIL import Image

img = Image.open(r"C:\Users\USER001\Downloads\codextest-main\codextest-main\gop.png")
img = img.resize((256,256))
img.save("app.ico")