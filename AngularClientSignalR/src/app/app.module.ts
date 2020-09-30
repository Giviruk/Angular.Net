import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {ChatComponent} from './chat/chat.component';
import {chatServices} from './services/chat-services';

@NgModule({
  declarations: [
    AppComponent,
    ChatComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: ChatComponent, pathMatch: 'full'}
    ])
  ],
  providers: [chatServices],
  bootstrap: [AppComponent]
})
export class AppModule {
}
