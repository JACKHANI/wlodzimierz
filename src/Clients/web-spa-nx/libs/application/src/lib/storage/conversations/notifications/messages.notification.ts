import { HttpErrorResponse } from '@angular/common/http';

import { ConversationMessagesListModel } from '@wlodzimierz/domain/src/lib/models/conversation-messages-list.model';

export interface MessagesNotification {

  onMessagesSuccess(messages: ConversationMessagesListModel): void;

  onMessagesFailed(error: HttpErrorResponse): void;
}