import {Component} from '@angular/core';
import {BehaviorSubject} from 'rxjs';
import {chatServices} from '../services/chat-services';
import {Message} from '../models/Message';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html'
})

export class ChatComponent {
  public userMessages: BehaviorSubject<Array<Message>>;

  constructor(private signalRService: chatServices) {
    this.userMessages = signalRService.userMessages;
  }
  title = 'chat-client';

  sendMessage(message: string, nickname: string) {
    this.signalRService.sendMessage(message, nickname);
  }

  ngOnInit() {
    this.signalRService.startConnection();
  }
}
