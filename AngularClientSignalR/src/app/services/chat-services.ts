import * as signalR from '@aspnet/signalr';
import { Message } from '../models/Message';
import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class chatServices {
  private hubConnection: signalR.HubConnection;
  public userMessages: BehaviorSubject<Array<Message>> = new BehaviorSubject<Array<Message>>(new Array<Message>());

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/chat')
      .build();

    this.hubConnection.start().then(() => {
      this.hubConnection.invoke('getData');
    });

    this.hubConnection.on('sendMessage', (message: string, nickname: string) => {
      const arr = this.userMessages.value;
      arr.push(new Message(nickname, message));
      this.userMessages.next(arr);
    });

    this.hubConnection.on('getData', (arr: Array<Message>) => {
      this.userMessages.next(arr);
    });
  }

  public sendMessage(message: string, nickname: string) {
    this.hubConnection.invoke('Send', message, nickname);
    console.log(this.userMessages.value);
  }
}
