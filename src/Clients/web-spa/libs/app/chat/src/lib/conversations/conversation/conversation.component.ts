import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';

import { Conversation } from '../shared/models/conversation.model';
import { User } from '../../../../../users/src/lib/shared/models/user.model';
import { ChangeConversationEvent } from '../shared/events/change-conversation.event';

@Component({
  selector: 'wlodzimierz-conversation',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './conversation.component.html',
  styleUrls: ['./conversation.component.scss']
})
export class ConversationComponent {
  @Input() public user: User;
  @Input() public conversation: Conversation;
  @Input() public bindingConversation: Conversation;
  @Output()
  public changeConversation: EventEmitter<ChangeConversationEvent> = new EventEmitter<ChangeConversationEvent>();

  public onChangeConversation(): void {
    this.changeConversation.emit({ conversation: this.conversation });
  }
}
