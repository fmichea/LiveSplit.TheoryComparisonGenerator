# Livesplit.TheoryComparisonGenerator
Theory Comparison Generator component for Livesplit to create theoretical splits for achievable goal times

**This is a pre-release version of the component**

[Download the DLL here](https://github.com/thags15/LiveSplit.TheoryComparisonGenerator/releases/latest)

## What are theory comparisons?
Theory comparisons are comparisons that you can access in LiveSplit through this plugin that are generated based on your sum of best and a given goal time. To generate each segment time, the plugin will go through each comparison you have created and perform the following formula to calculate what time each segment in your splits file should be in order for you to get the goal time:  
![image](https://user-images.githubusercontent.com/92997613/180649390-135a8ea9-3e81-4150-98d6-57b9df92f1cb.png)  

These aim to give you a comparison that is nice to run against and an idea of what times you should be expecting to reach said goal time.



## Adding the plugin to LiveSplit
Add the LiveSplit.TheoryComparisonGenerator.dll file to your LiveSplit's Components folder.

## Accessing the controls for the Theory Comparison Generator
1. Right click on LiveSplit and go to Edit Layout
2. Click the + button in the top left of the Layout Editor
3. Go to Other and select Theory Comparison Generator
4. Double click on the Theory Comparison Generator at the bottom of your layout editor's list of components

## Adding comparisons
On first open, your window should look like this.     
![image](https://user-images.githubusercontent.com/92997613/185758090-be072cfd-b491-4be9-8e95-0173a364b2b3.png)

The top checkmark box determines whether or not the component should automatically generate a "Theory PB" comparison. This comparison aims to be a better balanced pb comparison than LiveSplit's built in balanced comparison.  

In the empty box that says Theory Comparisons at the top, you can click the Add button to add a new Theory Comparison. This will create a new comparison entry that you can now edit with your desired time/name.  
![image](https://user-images.githubusercontent.com/92997613/180613050-2ac3db8d-8665-4d51-9d48-935c5d75ff5a.png)

The top field (Splits File) is not editable by typing in it. This takes your current splits file's name and automatically adds it to the comparison. Display Name and Goal Time are both editable. If you do not have a display name set, then the name your comparison will show in your layout would look something like Theory 20:00 (or whatever time you have set). Comparisons will only be generated if the "Goal Time" is entered in the right format (hh:mm:ss).  

Once you have your comparison set, you can either add another comparison and move the orders of the comparisons around (this will affect the order the comparisons are showed when you cycle through them in LiveSplit) or you can exit the layout editor by hitting OK.

## Accessing your comparisons
Right click your livesplit and go to Compare Against. You should see your generated comparisons in the middle section of the menu.  
![image](https://user-images.githubusercontent.com/92997613/180613539-5b770050-6d45-4b6e-9943-8f68d14a8f2f.png)  

From here, you can select to compare against these by clicking on them in this window, or cycling through your comparisons with your hotkeys. Comparisons should auto update after every reset if you get golds to generate new splits based on your new gold.  


## Other Features
When you make a new comparison it will automatically set the Splits Name for you. By default, the component settings window in the Layout Editor will only show comparisons that you have made for the current splits file. If you wanted to use a comparison you made for another splits file (say you changed routes and you wanted to use the old time you set) you can hit Show All in the settings menu to display every single comparison that you have made. From here, you would hit the button that says Attach to Current Splits to associate this comparison with your current splits. You should now see this comparison in LiveSplit.

## Notes
Comparisons are stored on the layout themselves, so if you use a different layout, you'd have to make new comparisons.  

It is advised to not use custom comparisons in your splits that are named such as "Theory PB" or "Goal Time" to avoid confusion with the auto generated splits by this component.
