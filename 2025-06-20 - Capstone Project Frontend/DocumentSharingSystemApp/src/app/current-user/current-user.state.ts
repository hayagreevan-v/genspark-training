import { Injectable } from '@angular/core';
import { State, Action, Selector, StateContext } from '@ngxs/store';
import { SetCurrentUserAction, RemoveCurrentUserAction } from './current-user.actions';
import { UserModel } from '../models/user.model';

export interface CurrentUserStateModel {
  user : UserModel | null
}

@State<CurrentUserStateModel>({
  name: 'currentUser',
  defaults: {
    user: null
  }
})
@Injectable()
export class CurrentUserState {

  @Selector()
  static getUser(state: CurrentUserStateModel) {
    return state.user;
  }

  @Action(SetCurrentUserAction)
  set(ctx: StateContext<CurrentUserStateModel>, { payload }: SetCurrentUserAction) {
    // let stateModel = ctx.getState();
    // stateModel.user =  payload;
    // console.log(payload);
    ctx.setState({user: payload});
  }
  @Action(RemoveCurrentUserAction)
  remove(ctx: StateContext<CurrentUserStateModel>) {
    ctx.setState({user:null});
  }

}
