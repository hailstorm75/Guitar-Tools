   M S B A M L   `   `   `  ���� G  CGuitarScales, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GuitarScalesGuitarScales      GuitarScales.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  )controlsclr-namespace:GuitarScales  -   $��
mainWindow��  � Title$  Guitar tools��$	��386��$	��525��$	��386��$	��525�� � WindowStartupLocation$ CenterScreen=� � Icon$ Resources/icon.png��
���   � � 	Resources  �� MergedDictionaries �� Source��  �� $ Assets/Animation.xaml?��� $ Assets/Theme.xaml?��� $ Assets/ComboBoxStyle.xaml?�%    .���� ���	#FF2B2424$��1q�._��  � RowDefinitions �� $��* �.+�� $��Stretch=�$��1��.+��� -   $��	Settings1��   
PanelStyle#��[  .+��� $��Scale�����	#FFC5BBCB$��28(�$	��250��$��50���� $��Center=�.+��� $	��220��.+��� $
��Root��$
��Left=�$��24(����� *ActualWidth+ �� ElementName$ lbTuning�� �� Mode$ OneWay=��� -   $��cbRoot��$	��100��  ComboBoxFlatStyle#��[ .@��� $��C���� $��C#���� $��D���� $��D#���� $��E���� $��F���� $��F#���� $��G���� $��G#���� $��A���� $��A#���� $��B���� $��0,10,0,0q�.+��� -   $��lbTuning��$��Tuning��$
��Left=�$��24(��� -   $��cbTuning��#��[ �� $��0,10,0,0q�.+��� -   $��lbScale��$��Scale��$
��Left=�$��24(����� *ActualWidth+$ lbTuning��$ OneWay=��� -   $��cbScale��#��[ �� -   $��	Settings2��#��[  .+��� $��Scale search�����	#FFC5BBCB$��28(�$	��250��$��50���� .+��� .@��� $
��Note��.���� �� $��Chord��.���� .+�� -	   $��MenuGrid��$	��200��	 � ColumnDefinitions	 �� �� .+��� $��0��.+��� -
   $��tbOne��-  'System.Windows.Input.KeyboardNavigation
   TabIndex$
 1��$��
0,10,2.5,0q� �� SelectedIndex$ 0��{�. #��[ .@��� $��- None -���� -   $��tbTwo��$
 3��$��
0,10,2.5,0q�$ 0��{�.  #��[ .@��� $��- None -���� -   $��tbThree��$
 5��$��
0,10,2.5,0q�$ 0��{�.  #��[ .@��� $��- None -���� $��1��.+��� -   $��cbOne��$
 2��$��
2.5,10,0,0q�$ 0��{�.  #��[ .@��� $��- None -���� -   $��cbTwo��$
 4��$��
2.5,10,0,0q�$ 0��{�.  #��[ .@��� $��- None -���� -   $��cbThree��$
 6��$��
2.5,10,0,0q�$ 0��{�.  #��[ .@��� $��- None -���� -   $��	btnSearch��$��Search��$��0, 10, 0,15q�{�.  $��Center=��� -   $��	lbResults��$	��100������ *ActualWidth+$ MenuGrid��$ OneWay=��� -   $��	Settings3��#��[  .+��� $��	Fretboard�����	#FFC5BBCB$��28(�$	��250��$��50���� $��Center=�$��5,0,10,0q�$��2������ *ActualWidth+$ 
stackPanel��$ OneWay=�.+�]� $	��220��$��Center=�.+��� .+��� $��Frets��$
��Left=�$��24(��� -    ��Name$ SliderFrets�� � Maximum$ 24Q� � Minimum$ 12Q� � Cursor$ SizeWE�� � SmallChange$ 1Q� � LargeChange$ 2Q� �� AutoToolTipPlacement$ BottomRight=� �� IsSnapToTickEnabled . $��Center=�$��0,4.5q��� $��Strings��$
��Left=�$��24(��� -   $ SliderStrings��$ 8Q�$ 6Q�$ SizeWE��$ 1Q�$ 2Q�$ BottomRight=� .  � Value$ 6Q�$��Center=�$��0,4.5q��� -   $��Apply��$��0,5,0,0q��� -   $��pnlLeftMenu��
�������$��50�� K� ZIndex$ 1��$
��Left=�.+��� -   ���	#FFC5BBCB  PanelOptionStyle#��[ .��]� $	��200��.+��� $��50��$��Center=���"  FontAwesome.WPF.ImageAwesome     Icon$
 Bars=�$��24���� $
��Menu��$��24(�$��Center=�$��5,0,0,0q�$	��140���� -   $��btnSettings1��#��[ .��]� $	��200��.+��� $��50��$��Center=���  $ Braille=�$��20���� $��Scale��$��24(�$��Center=�$��5,0,0,0q�$	��140���� -   $��btnSettings2��#��[ .��]� $	��200��.+��� $��50��$��Center=���  $ Search=�$��26���� $��Search scale��$��24(�$��Center=�$��5,0,0,0q�$	��140���� -   $��btnSettings3��#��[ .��]� $	��200��.+��� $��50��$��Center=���  $ Sliders=�$��24���� $��	Fretboard��$��24(�$��Center=�$��5,0,0,0q�$	��140��(�  (� Stretch$ Uniform=�$��50,0,0,0q�$��Center=�.��� -    �Name$ mainGrid��$	��525��$	��170��