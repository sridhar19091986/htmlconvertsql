%�����������
clc;
clear;
%load matlab;
yn = textread('D:\My Documents\MATLAB\yn.txt');
xite = textread('D:\My Documents\MATLAB\xite.txt');
%�̶�ϵ��
spread=0.999;
%mse=0.002; 
P=yn(:,[1:8,12]);
T=yn(:,9:11);  %T=yn(:,[9,12]);
Pt=xite(:,[1:8,9]);
input_train=P'; 
output_train=T'; 
input_test=Pt'; 
%ѡ����������������ݹ�һ�� 
%[inputn,inputps]=mapminmax(input_train); 
%[outputn,outputps]=mapminmax(output_train);
%����ѵ��
%net=newrb(input_train,output_train,mse,spread);
net=newgrnn(input_train,output_train,spread);
%net=newgrnn(inputn,outputn,spread);%newgrnn
%net=newrb(inputn,outputn,mse,spread); %newrb
%Ԥ�����ݹ�һ�� 
%inputn_test=mapminmax('apply',input_test,inputps); 
%����Ԥ����� 
output_test=sim(net,input_test);
output_test'
%inputn_test=sim(net,inputn_test);
%����һ��
%an=mapminmax('reverse',inputn_test,outputps);
%an'
%  ���������� 
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
%     title('�����Ԥ�����')
%     plot(sc,perf,'g:*');
% end
