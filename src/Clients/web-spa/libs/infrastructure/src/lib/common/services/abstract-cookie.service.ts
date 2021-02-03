import { CookieService } from 'ngx-cookie-service';

import { Cookie } from '@wlodzimierz/application/src/lib/common/interfaces/cookie.interface';

export abstract class AbstractCookieService<TModel> implements Cookie<TModel> {
  protected constructor(protected cookieName, protected cookieService: CookieService) {
  }

  public write(value: string, expires: Date): void {
    this.cookieService.set(this.cookieName, value, { path: '/', expires });
  }

  public check(): boolean {
    return this.cookieService.check(this.cookieName);
  }

  public clear(): void {
    this.cookieService.delete(this.cookieName);
  }

  public abstract read(): TModel;
}
