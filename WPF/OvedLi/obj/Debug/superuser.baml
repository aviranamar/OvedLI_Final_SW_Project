   M S B A M L   `   `   `  ����A  =OvedLi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:OvedLiOvedLi  5   	       OvedLi.superuser   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:OvedLi     5   	     � Title$)  #עובד לי מערכת ניהול��5   	   $��691.646��6%   $��1179.58��66    � Icon$A ;pack://application:,,,/OvedLi;component/Resources/users.ico��5	   	   ��5
      �� 5   
    �� ImageSource$F @pack://application:,,,/OvedLi;component/Resources/background.jpg��6   .��5      �  � ColumnDefinitions 5   
   �� 5      $��2* �6   �� 5      $��1* �6   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      $��3* �6   �� 5      $��1* �6   �� 5      $��1* �6   .+�5   
   �� -   5         ��Name$ logoimg��5   -   $��1��6   $��0��6    $
��Left=�6>   $	��100��6Y   $	��Top=�6g   $	��100��6   $L��Fpack://application:,,,/OvedLi;component/Resources/logo_transparent.png��6�   �� 5   
   -   5         ��Name$ companyEditComboBox��5   0   $��2��6   $��0��6#    �� IsDropDownOpen . $��25��6c   �� 5   
   -   5         ��Name$ compnayselectorlbl��5   -   $��2��6   $��0��6    $��אנא בחר חברה��6I   $
��Left=�6`   $	��Top=�6|   $��107,122,0,0q�6�   �� 5   
   -   5        $ label��5   -   $��1��6   $��0��6    $��הוסף חברה��6<   $
��Left=�6P   $	��Top=�6k   $��
98,130,0,0q�6�   �� 5   
   -   5        	 ��Name$	 companyaddbtn��5   /   $��1��6   $��0��6!   $��102,166,113,143q�6�   ��6d   Z� ��6z   Z� .��5      �� $G��Apack://application:,,,/OvedLi;component/Resources/add_company.png��6   $��64��6_   $��43��6k   5       �� 5"   
   -   5        $	 companyedit��5"   .   $��0��6   $��0��6!   $��239,186,268,135q�6�   ��6`   Z� ��6v   Z� .��5#      �� $H��Bpack://application:,,,/OvedLi;component/Resources/company_edit.png��6   $��64��6`   $��43��6l   5%      �� 5'   
   -   5        $ editcompanylbl��5'   -   $��0��6   $��0��6    $��ערוך חברה��6j   $
��Left=�6~   $	��Top=�6�   $��251,130,0,0q�6�   �� 5(   
   -   5        $ 
adduserlbl��5(   .   $��2��6   $��1��6!   $��הוסף משתמש��6B   $
��Left=�6W   $	��Top=�6r   $��
107,10,0,0q�6�   �� 5)   
   -	   5        $	 
adduserbtn��5)   .   $��2��6   $��1��6!   $��107,52,124,29q�6�   ��6_   Z� ��6u   Z� .��5*      �� $.��(/OvedLi;component/Resources/add-user.png��6   $��29��6F   $��28��6R   5+      �� 5,   
   -
   5        $ edituser��5,   .   $��1��6   $��1��6!   $��ערוך משתמש��6@   $
��Left=�6U   $	��Top=�6p   $��
107,10,0,0q�6�   �� 5-   
   -   5        $	 editusertbn��5-   .   $��1��6   $��1��6!   $��107,52,124,29q�6�   ��6a   Z� ��6w   Z� .��5.      �� $/��)/OvedLi;component/Resources/edit-user.png��6   $��29��6G   $��28��6S   5/      �� 50   
   -   5        $ disconnectlbl��50   .   $��0��6   $��1��6!   $��
התנתק��6E   $
��Left=�6U   $	��Top=�6p   $��
251,18,0,0q�6�   �� 51   
   -   5        $	 disconectbtn��51   .   $��0��6   $��1��6!   $��251,52,287,29q�6�   ��6c   Z� ��6y   Z� .��52      �� $-��'/OvedLi;component/Resources/connect.png��6   $��29��6E   $��28��6Q   53      54      56      