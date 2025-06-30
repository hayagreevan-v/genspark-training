import { UserModel } from "../models/user.model";

export class SetCurrentUserAction {
  static readonly type = '[CurrentUser] Set Current User';
  constructor(readonly payload: UserModel | null) { }
}
export class RemoveCurrentUserAction {
  static readonly type = '[CurrentUser] Remove Current User';
  constructor() { }
}
