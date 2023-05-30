import { useQuery, UseQueryOptions } from '@tanstack/react-query'
import { useFetchData } from './graphql-fetcher'
export type Maybe<T> = T | null
export type InputMaybe<T> = Maybe<T>
export type Exact<T extends Record<string, unknown>> = { [K in keyof T]: T[K] }
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> }
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> }
/** All built-in and custom scalars, mapped to their actual values */
export interface Scalars {
  ID: string
  String: string
  Boolean: boolean
  Int: number
  Float: number
  DateTime: any
}

export interface MyCustomTypeDto {
  __typename?: 'MyCustomTypeDTO'
  dateCreated: Scalars['DateTime']
  id: Scalars['Int']
}

export interface Query {
  __typename?: 'Query'
  accounts_getDataAnonymously?: Maybe<MyCustomTypeDto>
  authorization_addRole?: Maybe<Scalars['Boolean']>
  authorization_addRoleToUser?: Maybe<Scalars['Boolean']>
  authorization_createUser?: Maybe<Scalars['Boolean']>
  authorization_getDataAsAdmin?: Maybe<Scalars['String']>
  authorization_getDataAsVisitor?: Maybe<MyCustomTypeDto>
  authorization_getRoles?: Maybe<Array<Maybe<Scalars['String']>>>
}

export interface QueryAuthorization_AddRoleArgs {
  name?: InputMaybe<Scalars['String']>
}

export interface QueryAuthorization_AddRoleToUserArgs {
  id?: InputMaybe<Scalars['String']>
  role?: InputMaybe<Scalars['String']>
}

export interface QueryAuthorization_CreateUserArgs {
  email?: InputMaybe<Scalars['String']>
  password?: InputMaybe<Scalars['String']>
  role?: InputMaybe<Scalars['String']>
  userName?: InputMaybe<Scalars['String']>
}

export type About_Page_DataQueryVariables = Exact<Record<string, never>>

export interface About_Page_DataQuery { __typename?: 'Query', authorization_getDataAsVisitor?: { __typename?: 'MyCustomTypeDTO', id: number, dateCreated: any } | null }

export const About_Page_DataDocument = `
    query about_page_data {
  authorization_getDataAsVisitor {
    id
    dateCreated
  }
}
    `
export const useAbout_Page_DataQuery = <
      TData = About_Page_DataQuery,
      TError = unknown
    >(
    variables?: About_Page_DataQueryVariables,
    options?: UseQueryOptions<About_Page_DataQuery, TError, TData>
  ) =>
    useQuery<About_Page_DataQuery, TError, TData>(
      variables === undefined ? ['about_page_data'] : ['about_page_data', variables],
      useFetchData<About_Page_DataQuery, About_Page_DataQueryVariables>(About_Page_DataDocument).bind(null, variables),
      options
    )
