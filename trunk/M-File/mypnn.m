clc;
clear;
yn = textread('D:\My Documents\MATLAB\yn.txt');
xite = textread('D:\My Documents\MATLAB\xite.txt');
%P=yn(:,[1:8,12]);
P=yn(:,[1:8,12]);
T=yn(:,9);  %T=yn(:,[9,12]);
T=T+100;
%Pt=xite(:,[1:8,9]);
Pt=xite(:,[1:8,9]);
input_train=P'; 
output_train=ind2vec(T); 
input_test=Pt'; 
net=newpnn(input_train,output_train);
output=sim(net,input_test);
output_test=vec2ind(output);(output_test-100)'
SiT=sim(net,input_train);SimT=vec2ind(SiT)';
E = SimT - T;
perf=mse(E)