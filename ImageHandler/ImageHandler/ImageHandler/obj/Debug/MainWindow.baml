   M S B A M L   `   `   `  ����G  CImageHandler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ImageHandlerImageHandler  5   	       ImageHandler.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:ImageHandler     5   	     � Title$  
MainWindow��5   	   $	��700��6   $
��1300��6)   .��5	      � 
���   � � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $
��2.5* �6   .+�5   
   �� $��0��6   $��Hidden=�6'   .��5      �� -   5         ��Name$ imagesPanel��5   8   5      5      � 5   
   $��1��6    � RowDefinitions 5      �� 5      $��3* �6    �� 5      $��* �6    .+�5      �� -   5         ��Name$ 	MainImage��5   !   $��0��6   $��D:/3.jpg��64   6F   �� 5      $��1��6   .+�5      �� -   5         ��Name$ sizeOfImage��5   e   $3��-Размер файла в пикселях: ��6   
�������$��OpenSans)�6O   6|   �� 5      -   5        $ resolutionOfImage��5   ^   $'��!Разрешение файла: ��6   
�������$��OpenSans)�6H   6{   �� 5      -   5        $ currentPixel��5   b   $.��(Код текущего пикселя: ��6   
�������$��OpenSans)�6L   6z   �� 5      -   5        $ colorsCount��5   i   $;��5Различных цветов в картинке: ��6   
�������$��OpenSans)�6S   6�   �� 5      -   5         ��Name$ addPictures��5      $-��'Добавить изображения��6.   
�������
���iii�$��150px��6�   $
��Left=�6�   6�   �� 5       -   5        $ openMap��5       $��Открыть карту��6*   
�������
���iii�$��150px��6z   $
��Left=�6�   6�   5"      5#      5&      5'      