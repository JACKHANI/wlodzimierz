import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

import { AppComponent } from "./app.component";
import { NavTopMenuComponent } from "./layout/nav-top-menu/nav-top-menu.component";
import { NavFooterComponent } from "./layout/nav-footer/nav-footer.component";
import { AuthModule } from "./auth/auth.module";
import { ChatModule } from "./chat/chat.module";
import { DocsModule } from "./docs/docs.module";
import { HomeModule } from "./home/home.module";
import { AppRoutingModule } from "./app-routing.module";
import { StatusesModule } from "./statuses/statuses.module";
import { SettingsModule } from "./settings/settings.module";

@NgModule({
  declarations: [AppComponent, NavTopMenuComponent, NavFooterComponent],
  imports: [
    BrowserModule,
    NgbModule,
    AuthModule,
    ChatModule,
    DocsModule,
    HomeModule,
    SettingsModule,
    AppRoutingModule,
    StatusesModule,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
