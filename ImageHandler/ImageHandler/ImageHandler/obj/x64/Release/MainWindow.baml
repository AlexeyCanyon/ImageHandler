   M S B A M L   `   `   `  ���� G  CImageHandler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ImageHandlerImageHandler  T PCefSharp.Wpf, Version=69.0.0.0, Culture=neutral, PublicKeyToken=40c4b6fc221f4138A0clr-namespace:CefSharp.Wpf;assembly=CefSharp.WpfCefSharp.Wpf     ImageHandler.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:ImageHandler     EcefSharp0clr-namespace:CefSharp.Wpf;assembly=CefSharp.Wpf       � Title$  Главное окно��$	��768��$
��1366��.��� 
���   � � ColumnDefinitions �� $��* ��� $
��2.5* �.+��� $��0��$��Hidden=�.���� -    ��Name$ imagesPanel��� $��1�� � RowDefinitions �� $��3* ��� $��* �.+��� -    ��Name$ 	MainImage��$��0��$��D:/3.jpg���� $��1��.+��� -    ��Name$ sizeOfImage��$3��-Размер файла в пикселях: ��
�������$��OpenSans)��� -   $ resolutionOfImage��$'��!Разрешение файла: ��
�������$��OpenSans)��� -   $ currentPixel��$.��(Код текущего пикселя: ��
�������$��OpenSans)��� -   $ colorsCount��$;��5Различных цветов в картинке: ��
�������$��OpenSans)��� -    ��Name$ addPictures��$-��'Добавить изображения��
�������
���iii�$��150px��$
��Left=��� -   $ openMap��$��Открыть карту��
�������
���iii�$��150px��$
��Left=��� -	   $ changePicture��$ ��Редактировать��
�������
���iii�$��150px��$
��Left=��� -
   $ findFace��$#��Распознать лица��
�������
���iii�$��150px��$
��Left=��� $��1��$��250,0,336,0q�.+��� -   $ 	NameLabel��$'��!Название картины: ��
�������$��OpenSans)��� -   $ AuthorLabel��$!��Автор картины: ��
�������$��OpenSans)��� -   $ YearOfCreationLabel��$!��Дата создания: ��
�������$��OpenSans)��� -   $ MaterialLabel��$��Материал: ��
�������$��OpenSans)��� -   $ DescriptionLabel��$��Описание: ��
�������$��OpenSans)��� -   $ 
RulesLabel��$1��+Правила использования: ��
�������$��OpenSans)��� $��1��$��	639,0,0,0q�.+��� -   $ PercentOfRedLabel��$1��+Насыщенность красного: ��
�������$��OpenSans)��� -   $ PercentOfGreenLabel��$1��+Насыщенность зеленого: ��
�������$��OpenSans)��� -   $ PercentOfBlueLabel��$-��'Насыщенность синего: ��
�������$��OpenSans)��� -   $ PlaceOfStorageLabel��$#��Место хранения: ��
�������$��OpenSans)��� -   $ PlaceOfCreationLabel��$#��Место создания: ��
�������$��OpenSans)�