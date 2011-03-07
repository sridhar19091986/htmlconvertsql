function cf tool  fitting(X,Y)
%CF TOOL  FITTING    Create plot of datasets and fits
%   CF TOOL  FITTING(X,Y)
%   Creates a plot, similar to the plot in the main curve fitting
%   window, using the data that you provide as input.  You can
%   apply this function to the same data you used with cftool
%   or with different data.  You may want to edit the function to
%   customize the code and this help message.
%
%   Number of datasets:  1
%   Number of fits:  3

 
% Data from dataset "Y vs. X":
%    X = X:
%    Y = Y:
%    Unweighted
%
% This function was automatically generated on 07-Mar-2011 16:29:08

% Set up figure to receive datasets and fits
f_ = clf;
figure(f_);
set(f_,'Units','Pixels','Position',[469.333 115 680 484]);
legh_ = []; legt_ = {};   % handles and text for legend
xlim_ = [Inf -Inf];       % limits of x axis
ax_ = axes;
set(ax_,'Units','normalized','OuterPosition',[0 0 1 1]);
set(ax_,'Box','on');
axes(ax_); hold on;

 
% --- Plot data originally in dataset "Y vs. X"
X = X(:);
Y = Y(:);
h_ = line(X,Y,'Parent',ax_,'Color',[0.333333 0 0.666667],...
     'LineStyle','none', 'LineWidth',1,...
     'Marker','.', 'MarkerSize',12);
xlim_(1) = min(xlim_(1),min(X));
xlim_(2) = max(xlim_(2),max(X));
legh_(end+1) = h_;
legt_{end+1} = 'Y vs. X';

% Nudge axis limits beyond data limits
if all(isfinite(xlim_))
   xlim_ = xlim_ + [-1 1] * 0.01 * diff(xlim_);
   set(ax_,'XLim',xlim_)
else
    set(ax_, 'XLim',[0.80000000000000004, 21.199999999999999]);
end


% --- Create fit "fit 1"
ok_ = isfinite(X) & isfinite(Y);
if ~all( ok_ )
    warning( 'GenerateMFile:IgnoringNansAndInfs', ...
        'Ignoring NaNs and Infs in data' );
end
ft_ = fittype('poly7');

% Fit this model using new data
cf_ = fit(X(ok_),Y(ok_),ft_);

% Or use coefficients from the original fit:
if 0
   cv_ = { 4.9703329081409031e-006, -0.00037925571957720159, 0.011350050851760694, -0.16767488396315733, 1.2528558875870184, -4.1511639532103057, 2.6450539704645979, 13.810216718267506};
   cf_ = cfit(ft_,cv_{:});
end

% Plot this fit
h_ = plot(cf_,'fit',0.95);
legend off;  % turn off legend from plot method call
set(h_(1),'Color',[1 0 0],...
     'LineStyle','-', 'LineWidth',2,...
     'Marker','none', 'MarkerSize',6);
legh_(end+1) = h_(1);
legt_{end+1} = 'fit 1';

% --- Create fit "fit 1 copy 1"
ok_ = isfinite(X) & isfinite(Y);
if ~all( ok_ )
    warning( 'GenerateMFile:IgnoringNansAndInfs', ...
        'Ignoring NaNs and Infs in data' );
end
ft_ = fittype('poly7');

% Fit this model using new data
cf_ = fit(X(ok_),Y(ok_),ft_);

% Or use coefficients from the original fit:
if 0
   cv_ = { 4.9703329081409031e-006, -0.00037925571957720159, 0.011350050851760694, -0.16767488396315733, 1.2528558875870184, -4.1511639532103057, 2.6450539704645979, 13.810216718267506};
   cf_ = cfit(ft_,cv_{:});
end

% Plot this fit
h_ = plot(cf_,'fit',0.95);
legend off;  % turn off legend from plot method call
set(h_(1),'Color',[0 0 1],...
     'LineStyle','-', 'LineWidth',2,...
     'Marker','none', 'MarkerSize',6);
legh_(end+1) = h_(1);
legt_{end+1} = 'fit 1 copy 1';

% --- Create fit "fit 1 copy 2"
ok_ = isfinite(X) & isfinite(Y);
if ~all( ok_ )
    warning( 'GenerateMFile:IgnoringNansAndInfs', ...
        'Ignoring NaNs and Infs in data' );
end
ft_ = fittype('poly7');

% Fit this model using new data
cf_ = fit(X(ok_),Y(ok_),ft_);

% Or use coefficients from the original fit:
if 0
   cv_ = { 4.9703329081409031e-006, -0.00037925571957720159, 0.011350050851760694, -0.16767488396315733, 1.2528558875870184, -4.1511639532103057, 2.6450539704645979, 13.810216718267506};
   cf_ = cfit(ft_,cv_{:});
end

% Plot this fit
h_ = plot(cf_,'fit',0.95);
legend off;  % turn off legend from plot method call
set(h_(1),'Color',[0.666667 0.333333 0],...
     'LineStyle','-', 'LineWidth',2,...
     'Marker','none', 'MarkerSize',6);
legh_(end+1) = h_(1);
legt_{end+1} = 'fit 1 copy 2';

% Done plotting data and fits.  Now finish up loose ends.
hold off;
leginfo_ = {'Orientation', 'vertical', 'Location', 'NorthEast'}; 
h_ = legend(ax_,legh_,legt_,leginfo_{:});  % create legend
set(h_,'Interpreter','none');
xlabel(ax_,'');               % remove x label
ylabel(ax_,'');               % remove y label
