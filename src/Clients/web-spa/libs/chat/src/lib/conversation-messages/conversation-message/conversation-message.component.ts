import { Component, Input } from '@angular/core';

import { ConversationMessage } from '../shared/models/conversation-message.model';
import { User } from '../../../../../users/src/lib/shared/models/user.model';

@Component({
  selector: 'wlodzimierz-conversation-message',
  templateUrl: './conversation-message.component.html',
  styleUrls: ['./conversation-message.component.scss']
})
export class ConversationMessageComponent {
  @Input() public message: ConversationMessage;
  @Input() public user: User;
}
