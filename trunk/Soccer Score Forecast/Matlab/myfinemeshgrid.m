function [row,col] = myfinemeshgrid(vectorstepx, vectorstepy)
%Using two colons to create a vector with increments between
%first and end elements.
[X,Y] = meshgrid( vectorstepx(1):vectorstepx(2):vectorstepx(3), ...
vectorstepy(1):vectorstepy(2):vectorstepy(3) ) ;
[row, col] = size(X) ;