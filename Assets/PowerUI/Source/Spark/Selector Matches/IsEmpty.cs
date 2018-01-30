//--------------------------------------//               PowerUI////        For documentation or //    if you have any issues, visit//        powerUI.kulestar.com////    Copyright � 2013 Kulestar Ltd//          www.kulestar.com//--------------------------------------using System;namespace Css{		/// <summary>	/// Describes if an element currently is empty	/// <summary>	sealed class IsEmpty:CssKeyword{				public override string Name{			get{				return "empty";			}		}				public override SelectorMatcher GetSelectorMatcher(){			return new EmptyMatcher();		}			}		/// <summary>	/// Handles the matching process for :empty.	/// </summary>		sealed class EmptyMatcher:LocalMatcher{				public override bool TryMatch(Dom.Node node){						if(node==null){				return false;			}						return node["value"]==null;					}			}	}