%径向基神经网络
clc;
clear;
%load matlab;
yn = textread('D:\My Documents\MATLAB\yn.txt');
xite = textread('D:\My Documents\MATLAB\xite.txt');
%固定系数
spread=0.999;
%mse=0.002; 
P=yn(:,[1:8,12]);
T=yn(:,9:11);  %T=yn(:,[9,12]);
Pt=xite(:,[1:8,9]);
input_train=P'; 
output_train=T'; 
input_test=Pt'; 
%选连样本输入输出数据归一化 
%[inputn,inputps]=mapminmax(input_train); 
%[outputn,outputps]=mapminmax(output_train);
%网络训练
%net=newrb(input_train,output_train,mse,spread);
net=newgrnn(input_train,output_train,spread);
%net=newgrnn(inputn,outputn,spread);%newgrnn
%net=newrb(inputn,outputn,mse,spread); %newrb
%预测数据归一化 
%inputn_test=mapminmax('apply',input_test,inputps); 
%网络预测输出 
output_test=sim(net,input_test);
output_test'
%inputn_test=sim(net,inputn_test);
%反归一化
%an=mapminmax('reverse',inputn_test,outputps);
%an'
%  计算仿真误差 
SiT=sim(net,input_train);SimT=SiT';
E = SimT - T;
perf=mse(E)
% for sc=0.1:0.01:1;
%     net=newgrnn(input_train,output_train,sc);
%     SiT=sim(net,input_train);SimT=SiT';
%     E = SimT - T;
%     perf=mse(E);
%     hold on;
%     figure(1);
%     title('网络的预测误差')
%     plot(sc,perf,'g:*');
% end
