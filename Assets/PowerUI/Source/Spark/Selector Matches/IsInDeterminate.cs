//--------------------------------------//               PowerUI////        For documentation or //    if you have any issues, visit//        powerUI.kulestar.com////    Copyright � 2013 Kulestar Ltd//          www.kulestar.com//--------------------------------------using System;namespace Css{		/// <summary>	/// Describes if an element is indeterminate	/// <summary>	sealed class IsIndeterminate:CssKeyword{				public override string Name{			get{				return "indeterminate";			}		}				public override SelectorMatcher GetSelectorMatcher(){			return new IndeterminateMatcher();		}			}		/// <summary>	/// Handles the matching process for :indeterminate.	/// </summary>		sealed class IndeterminateMatcher:LocalMatcher{				public override bool TryMatch(Dom.Node node){						if(node==null){				return false;			}						return (node["indeterminate"]=="1");					}			}	}