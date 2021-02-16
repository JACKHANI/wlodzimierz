import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';

import { ConversationMessage } from '../shared/models/conversation-message.model';
import { Conversation } from '../../conversations/shared/models/conversation.model';
import { User } from '../../../../../users/src/lib/shared/models/user.model';
import { CreateConversationMessageEvent } from '../shared/events/create-conversation-message.event';
import { defaultModel } from '../../../../../storage/src/lib/common/defaults/model.default';

@Component({
  selector: 'wlodzimierz-conversation-message-create',
  templateUrl: './conversation-message-create.component.html',
  styleUrls: ['./conversation-message-create.component.scss']
})
export class ConversationMessageCreateComponent implements OnInit {
  @Input() public conversation: Conversation;
  @Input() public user: User;

  @Output()
  public createConversationMessage: EventEmitter<CreateConversationMessageEvent> = new EventEmitter<CreateConversationMessageEvent>();

  public formGroup: FormGroup;
  public messageModel: ConversationMessage = defaultModel();

  public get message(): AbstractControl {
    return this.formGroup.get('message') as AbstractControl;
  }

  public ngOnInit(): void {
    this.setupForm();
  }

  public onSendMessage(): void {
    if (this.formGroup.invalid) {
      return;
    }

    this.messageModel = this.formGroup.getRawValue() as ConversationMessage;
    this.messageModel.conversation = this.conversation;
    this.messageModel.ownerUserId = this.user.userId;
    this.createConversationMessage.emit({ message: this.messageModel });
  }

  ///////////////////////////////////////////////////////////////////////////
  // Helpers
  ///////////////////////////////////////////////////////////////////////////

  private setupForm(): void {
    this.formGroup = new FormGroup({ message: new FormControl(this.messageModel.message, [Validators.required]) });
  }
}
