   M S B A M L   `   `   `  ���� G  CImageHandler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ImageHandlerImageHandler  &     ImageHandler.ChangePictureWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:ImageHandler       � Title$1  +Редактирование картины��$	��516��$	��588��.��� 
���   �$��0,0,0,-2q� � ColumnDefinitions �� $
��317* ��� $	��79* �.+���  J� RenderTransformOrigin$ 0.204,0.513&�$��
10,0,275,0q�z�V� .~��� ��  �� AngleY$ 0.127Q��� T�  T� Y$ 0.239Q�.+��� -    ��Name$ 	NameLabel��$'��!Название картины: ��
�������$��OpenSans)��� -    ��Name$ NameText��$��23��$��
10,0,-60,0q� �� MaxLines$ 1���� -   $ AuthorLabel��$!��Автор картины: ��
�������$��OpenSans)��� -   $ 
AuthorText��$��23�� �� TextWrapping$
 Wrap=�$��
10,0,-60,0q��� -   $ YearOfCreationLabel��$!��Дата создания: ��
�������$��OpenSans)��� -   $ YearOfCreationText��$��23��$
 Wrap=�$��
10,0,-60,0q��� -   $ MaterialLabel��$��Материал: ��
�������$��OpenSans)��� -   $ MaterialText��$��23��$
 Wrap=�$��
10,0,-60,0q��� -	   $ DescriptionLabel��$��Описание: ��
�������$��OpenSans)��� -
   $ DescriptionText��$��23��$
 Wrap=�$��
10,0,-60,0q��� -   $ 
RulesLabel��$1��+Правила использования: ��
�������$��OpenSans)��� -   $ 	RulesText��$��23��$
 Wrap=�$��
10,0,-60,0q��� -   $ TechnologyLabel��$��Технология: ��
�������$��OpenSans)��� -   $ TechnologyText��$��23��$
 Wrap=�$��
10,0,-60,0q��� -   $ PlaceOfStorageLabel��$#��Место хранения: ��
�������$��OpenSans)��� -   $ PlaceOfStorageText��$��23��$
 Wrap=�$��
10,0,-60,0q��� -   $ PlaceOfCreationLabel��$#��Место создания: ��
�������$��OpenSans)��� -   $ PlaceOfCreationText��$��23��$
 Wrap=�$��
10,0,-60,0q��� -   $+��%Сохранить изменения��$��	10,15,0,0q�
�������
���iii�$��36��$��14(�$��Arial)��� $��
351,0,10,0q�$��2��.+��� $7��1Свойства для работы карты: ��
�������$��OpenSans)��� -   $" latitudePlaceOfCreationLabel��$0��*Широта места создания: ��
�������$��OpenSans)��� -   $$ latitudePlaceOfCreationTextbox��$��23��$
 Wrap=�$��
10,0,118,0q��� -   $# longitudePlaceOfCreationLabel��$2��,Долгота места создания: ��
�������$��OpenSans)��� -   $% longitudePlaceOfCreationTextbox��$��23��$
 Wrap=�$��
10,0,118,0q��� -   $! latitudePlaceOfStorageLabel��$0��*Широта места хранения: ��
�������$��OpenSans)��� -   $# latitudePlaceOfStorageTextbox��$��23��$
 Wrap=�$��
10,0,118,0q��� -   $" longitudePlaceOfStorageLabel��$2��,Долгота места хранения: ��
�������$��OpenSans)��� -   $$ longitudePlaceOfStorageTextbox��$��23��$
 Wrap=�$��
10,0,118,0q��� -   $ MapYearOfCreationLabel��$��Год создания: ��
�������$��OpenSans)��� -   $ MapYearOfCreationTextbox��$��23��$
 Wrap=�$��
10,0,118,0q��� -   $ HeightPictureLabel��$#��Высота картины: ��
�������$��OpenSans)��� -   $ HeightPictureTextbox��$��23��$
 Wrap=�$��
10,0,118,0q��� -    $ WidthPictureLabel��$#��Ширина картины: ��
�������$��OpenSans)��� -!   $ WidthPictureTextbox��$��23��$
 Wrap=�$��
10,0,118,0q��� -"   $ SourceSizePictureLabel��$'��!Исходные размеры: ��
�������$��OpenSans)�